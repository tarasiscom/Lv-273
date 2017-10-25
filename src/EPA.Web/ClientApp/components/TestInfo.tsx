﻿import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';

<<<<<<< HEAD

=======
>>>>>>> origin/Page_Test_Information_UI_REACT
interface TestDetailInformation {
    id: number;
    name: string;
    description: string;
    approximatedTime: number;
    questionsCount: number;
    loading: boolean;
}

export class TestInfo extends React.Component<RouteComponentProps<{}>, TestDetailInformation> {
    constructor() {
        super();
        this.state = {
<<<<<<< HEAD
            id: 0, name: "", description: "", approximatedTime: 0, questionsCount: 0, loading: true};
    }
    componentDidMount() {
        this.fetchData();
    }

    fetchData() {
        let pathId = this.props.match.params['id'];
=======
            id: 0, name: "", description: "", approximatedTime: 0, questionsCount: 0, loading: true
        };

        let pathId = window.location.pathname.substr(10, window.location.pathname.length);
        
>>>>>>> origin/Page_Test_Information_UI_REACT
        let path = 'api/profTest/' + pathId + '/info';
        fetch(path)
            .then(response => response.json() as Promise<TestDetailInformation>)
            .then(data => {
                this.setState({ id: data.id, name: data.name, description: data.description, approximatedTime: data.approximatedTime, questionsCount: data.questionsCount, loading: false });
            });

    }

    public render() {
        return <div>
            <div className="jumbotron jumbotron-fluid">
                <div className="container">
                    <h1 className="display-1">{this.state.name}</h1>
                    <p>
                        <Link to={'/profTest/' + this.state.id} className="btn btn-primary">Розпочати тест</Link>
                    </p>
                </div>
            </div>
            <section className="container-fluid">
                <div className="container">
                    <p className="text-left text-muted h4">Код Тесту: {this.state.id}</p>
                    <p className="text-left text-muted h4">Час на виконання: {this.state.approximatedTime} </p>
                    <p className="text-left text-muted h4">Кількість питань: {this.state.questionsCount}</p>
                    <p className="text-left text-muted h4">Опис: {this.state.description} </p>
                </div>
            </section>
        </div>
    }
}


