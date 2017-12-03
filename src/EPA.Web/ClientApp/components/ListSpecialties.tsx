import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Tabbordion, TabPanel, TabLabel, TabContent } from 'react-tabbordion';
import {GetFetch} from './App'

interface Subject {
    id: number;
    name: string
}

interface Specialty {
    id: number;
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
    isFavorite: boolean;
}

interface Specialties
{
    specialties: Specialty[];
}

interface Authenticator {

    isAuthenticated: boolean;
}

export default class ListSpecialties extends React.Component<Specialties, Specialties & Authenticator> {

    constructor(props) {
        super(props);



        this.state = { specialties: this.props.specialties, isAuthenticated:false };
    }

    componentWillReceiveProps(nextProps) {
            this.setState({specialties: (nextProps as Specialties).specialties })
    }

    componentWillMount() {
        GetFetch<any>('api/CheckAuth').then(data => this.setState({ isAuthenticated: data }));
    }

    private favHandle(id){
        //alert(id);
        let specs = this.state.specialties;
        let path = specs[id].isFavorite ? 'api/user/AddToFav/' : 'api/user/RemoveFromFav/'
        specs[id].isFavorite = !specs[id].isFavorite;
        GetFetch(path + specs[id].id).then(data => {
            if (data)
                this.setState({ specialties: specs });
            else
                alert('not happening');
        })
    }

    public render() {

        return <div className="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <Tabbordion animateContent="height" className="accordion" mode="toggle" role="tablist">
                {this.state.specialties.map((specialty, id) =>
                    <TabPanel key={id} className="my-cursor">
                        <div className="panel panel-default">
                            <div className="panel-heading" >
                                {this.state.isAuthenticated? <div className="favButton" onClick={() => this.favHandle(id)}>{specialty.isFavorite ? "-" : "+"}</div> : <div></div>}
                                
                                <TabLabel className="my-cursor panel-heading tab-width" >
                                    <p>Спеціальність: {specialty.name} </p>
                                    <p>Університет: {specialty.university}</p>
                                    <p>Область: {specialty.district}</p>
                                </TabLabel >
                            </div>
                         <TabContent>
                                <div className="panel-body">
                                    <div className="col-md-6">
                                        <p>Адреса: {specialty.address}</p>
                                        <p>Сайт:
                                         <a target="_blank" href={specialty.site}> {specialty.site} </a>
                                        </p>
                                    </div>
                                    <div className="col-md-6">
                                        <ul>Предмети: {specialty.subjects.map((sub, id) =>
                                            <li key={id}> {sub.name} </li>)}
                                        </ul>
                                    </div>
                                </div>
                        </TabContent>
                      </div> 
                    </TabPanel>
            )}
            </Tabbordion>  
        </div>
    }
}
