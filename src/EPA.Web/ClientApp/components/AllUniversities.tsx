import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
import { Loading } from './Loading';
import { Link, NavLink } from 'react-router-dom';


interface StateTypes {
    universities: University[];
    loading: boolean;
    hoveredUniversity: number;
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
            loading: true,
            hoveredUniversity: -1
        }
    }

    loadUniversities() {
        // let pathId = this.props.match.params['direction'];
        let path = 'api/universities/2';

        GetFetch<any>(path)
            .then(data => {
                this.setState({
                    universities: data,                   
                    loading: false
                });
            })
            .catch(er => this.props.onError(er))

    }

    componentWillMount() {
        this.loadUniversities();
    }

    hoverHandler(cardId: number) {
        this.setState({
            hoveredUniversity: cardId
        });
    }

    render() {
        const { hoveredUniversity, universities, loading } = this.state
        if (loading) {
            return <Loading />
        }
        else {
            const listUniversities = universities.map((item, id) => {
                return (
                    <div key={id} className="university-card" onMouseOver={() => this.hoverHandler(id)}>
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