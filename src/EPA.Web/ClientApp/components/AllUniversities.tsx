import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
import { Loading } from './Loading';
import { Link, NavLink } from 'react-router-dom';
import VirtualizedSelect from 'react-virtualized-select';
import 'react-select/dist/react-select.css';
import 'react-virtualized-select/styles.css';
import 'react-virtualized/styles.css';

interface DistrictDTO {
    name: string;
    id: number;
}

class District {
    value: number;
    label: string;
    constructor(value: number, label: string) {
        this.value = value;
        this.label = label;
    }
}

interface StateTypes {
    universities: University[];
    loading: boolean;
    hoveredUniversity: number;
    districts: District[];
    selectDistrict: District;
    loadingDistricts: boolean;
}

interface University {
    id: number;
    name: string;
    address: string;
    rating: number;
    site: string;
}

export class AllUniversities extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {
    constructor() {
        super();
        this.state = {
            universities: [],
            loading: false,
            hoveredUniversity: -1,
            districts: [],
            selectDistrict: { label: "Виберіть область", value: -1 },
            loadingDistricts: true
        }
    }

    loadUniversities(districtid) {
        let path = 'api/universities/' + districtid;
        console.log(districtid);
        GetFetch<any>(path)
            .then(data => {
                this.setState({
                    universities: data,                   
                    loading: false,
                    selectDistrict: districtid
                });
            })
            .catch(er => this.props.onError(er))

    }

    componentDidMount() {
        this.fetchAllDistricts();
    }

    private fetchAllDistricts() {
        GetFetch<DistrictDTO[]>('api/ChooseSpecialties/districtsList')
            .then(data => {
                this.setState({
                    districts: data.map<District>(district => new District(district.id, district.name))
                    .filter(function (dis) {
                        return dis.label !="Всі";
                    }),
                    loadingDistricts: false
                })
            })
            .catch(er => this.props.onError(er))
    }
    someHandler(cardId: number) {
        this.setState({
            hoveredUniversity: cardId
        });
    }
    handleOnChangeDistrict = (selectDistricty) => {
        this.setState({ loading: true })
        this.loadUniversities(selectDistricty.value)
    }
    public render() {
        return <div>
            <div className="delete-margin">
                <section className="jumbotron center-block">
                    <div className="container content-center">
                        <div className="col-md-4 col-sm-4  col-xs-8 navigate">
                            <div className="virtselect"><p className="text-center">Області</p>
                            <VirtualizedSelect multi={false}
                                options={this.state.districts}
                                onChange={this.handleOnChangeDistrict}
                                value={this.state.selectDistrict} >
                            </VirtualizedSelect>
                        </div>
                     </div> 
                   </div>
                </section>
               </div>
               <div>
                {this.renderAllUniversities()}
                </div>
        </div>
    }

    renderAllUniversities() {
        const { hoveredUniversity, universities, loading } = this.state
        if (loading) {
            return <Loading />
        }
        else {
            const listUniversities = universities.map((item, id) => {
                return (
                    <div key={id} className="university-card" onMouseOver={() => this.someHandler(id)}>
                        <div className="flip">
                            <div className={(id == hoveredUniversity ? "card flipped" : "card")}>
                                <div className="face front align-middle">
                                    <div className="inner">
                                        <h4 className="align-middle">{item.name}</h4>
                                    </div>
                                </div>
                                <div className="face back">
                                    <div className="inner text-center">
                                        <p>Рейтинг: {item.rating}</p>
                                        <p>Адреса: {item.address}</p>
                                        <p>Сайт: <a>{item.site}</a></p>
                                        <Link to={'/University/' + item.id} className="btn btn-link btn-block">Розпочати</Link>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                );
            });
            return <div className="pad-for-footer"><div className="universities-container">{listUniversities}</div></div>
        }
    }
};