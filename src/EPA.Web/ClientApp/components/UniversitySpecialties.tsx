import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
import { Loading } from './Loading';
import ListSpecialties from './ListSpecialties';


interface StateTypes {
    universityId: number;
    directionId: number;
    specialties: Specialty[];
    count: Count;
    loading: boolean;
    directions: GeneralDirection[];
}

interface Specialty {
    id: number;
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
    isFavorite: boolean
}

interface Subject {
    id: number;
    name: string
}

interface Count {
    allElements: number;
    forOnePage: number;
}

interface GeneralDirection {
    name: string;
    id: number;
}

export class UniversitySpecialties extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {
    constructor() {
        super();
        this.state = {
            universityId: -1,
            directionId: -1,
            specialties: [],
            count: { allElements: 1, forOnePage: 1 },
            loading: true,
            directions: []
        }
    }

    fetchDirections() {
        GetFetch<any>('api/ChooseSpecialties/directionsList')
            .then(data => {
                this.setState({
                    directions: data,
                    loading: false
                });
            })
            .catch(er => this.props.onError(er))

    }

    fetchCount() {
        GetFetch<any>('api/GetSpecialties/count/' + this.state.universityId + '/' + this.state.directionId)
            .then(data => {
                this.setState({
                    count: data
                });
            })
            .catch(er => this.props.onError(er));
    }

    fetchSpecialties() {
        GetFetch<any>('api/GetSpecialties/' + this.state.universityId + '/' + this.state.directionId)
            .then(data => {
                this.setState({
                    specialties: data
                });
            })
            .catch(er => this.props.onError(er))
    }

    loadSpecialties() {
        // this.fetchCount();
        this.fetchSpecialties();
    }

    componentWillMount() {
        this.fetchDirections();
        this.setState({
            directionId: this.state.directions[0].id,
            universityId: this.props.match.params['universityId']
        });
        this.loadSpecialties();
    }

    render() {
        const { universityId, directionId, specialties, count, loading, directions } = this.state
        if (loading) {
            return <Loading />
        }
        else {
            const listDirection = directions.map((item, id) => {
                return (
                    <div key={id} className="item">
                        <span><img src={"/pictures/directions/" + item.id + ".png"} /></span>
                        <p>{item.name}</p></div>
                );
            });
            return <div>
                <div className="left">
                    {listDirection}
                </div>
                <div className="specielties-container">
                    <ListSpecialties specialties={specialties}></ListSpecialties>
                </div>
            </div>
        }
    }
};
