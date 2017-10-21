import * as React from 'react';
import { RouteComponentProps, withRouter, Switch } from 'react-router';
 
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';





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
            id: 0, name: "", description: "", approximatedTime: 0, questionsCount: 0, loading: true
        };



        let pathId = window.location.pathname.substr(10, window.location.pathname.length);
        let path = 'api/profTest/' + pathId + '/info';
        fetch(path)
            .then(response => response.json() as Promise<TestDetailInformation>)
            .then(data => {
                this.setState({ id: data.id, name: data.name, description: data.description, approximatedTime: data.approximatedTime, questionsCount: data.questionsCount, loading: false });
            });
    }

    public render() {
        return <div>
            <h1 className="text-center">Профорієнтаційний тест № {this.state.id}</h1>
            <hr></hr>
            <section className="container-fluid">
                <div className="row justify-content-start align-items-center">
                    <div className="col-md-6">
                        <h3 className="text-center">Номер тесту</h3>
                        <h3 className="text-center">Імя </h3>
                        <h3 className="text-center">Час на виконання </h3>
                        <h3 className="text-center">Кількість питань </h3>
                    </div>
                    <div className="col-md-6">
                        <h3 className="text-center">{this.state.id}</h3>
                        <h3 className="text-center">{this.state.name} </h3>
                        <h3 className="text-center">{this.state.approximatedTime} </h3>
                        <h3 className="text-center">{this.state.questionsCount} </h3>
                    </div>

                </div>
            </section>

            

        </div>
    }

}
    

