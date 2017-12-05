import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
import { Loading } from './Loading';
import ListSpecialties from './ListSpecialties';


interface StateTypes {
    directionId: number;
    specialties: Specialty[];
    count: Count;
    loading: boolean;
    directions: GeneralDirection[];
    loadingForSpecialties: boolean;
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
            directionId: 1,
            specialties: [],
            count: { allElements: 1, forOnePage: 1 },
            loading: true,
            directions: [],
            loadingForSpecialties: true
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

    fetchSpecialties(direction:number) {
        const universityId = this.props.match.params['universityId'];
        GetFetch<any>('api/GetSpecialties/' + universityId + '/' + direction)
            .then(data => {
                this.setState({
                    specialties: data,
                    directionId: direction,
                    loadingForSpecialties: false
                });
            })
            .catch(er => this.props.onError(er))
    }

    componentWillMount() {
        this.fetchDirections();
        this.fetchSpecialties(this.state.directionId);
    }

    changeDirection(id: number) {
        this.setState({
            loadingForSpecialties: true
        });
        this.fetchSpecialties(id);
    }

    render() {
        const { directionId, specialties, count, loading, directions, loadingForSpecialties } = this.state
        if (loading) {
            return <Loading />
        }
        else {
            const listDirection = directions.map((item, id) => {
                return (
                    <div key={id} className="item" onClick={() => this.changeDirection(directions[id].id)}>
                        <span><img src={"/pictures/directions/" + item.id + ".png"} /></span>
                        <p>{item.name}</p></div>
                );
            });

            let specialtiesList = <div></div>;
            if (loadingForSpecialties == true) {
                specialtiesList = < Loading />;
            }
            else {
                if (specialties.length != 0) {
                    specialtiesList = <ListSpecialties specialties={specialties}></ListSpecialties>;
                }
                else {
                    let directionName;
                    for (let i = 0; i < directions.length; i++) {
                        if (directions[i].id == directionId)
                            directionName = directions[i].name;
                    }
                    specialtiesList = <h4 className="pad-for-nav col-md-offset-1">В цьому університеті немає спеціальностей в галузі '{directionName}'</h4 >
                }
            }

            return <div>
                <div className="left">
                    {listDirection}
                </div>
                <div className="specielties-container pad-for-footer">
                    {specialtiesList}
                </div>
            </div>
        }
    }
};
