/// <reference path="personalcabinet.tsx" />
import * as React from 'react';
import { RouteComponentProps, withRouter, Switch, Redirect } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import { ErrorHandlerProp, GetFetch } from './App';
import { Loading } from './Loading';

//import { Error404inComp } from './errors/404';

interface TestDetailInformation {
    id: number;
    name: string;
    description: string;
    approximateTime: number;
    questionsCount: number;
    loading: boolean;
}
/*
interface ErrorHandlerProp {
    onError: PropTypes.func    
}*/

export class TestInfo extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, TestDetailInformation> {
    constructor(props) {
        super();
        this.state = {
            id: 0, name: "", description: "", approximateTime: 0, questionsCount: 0, loading: true
        };
    }

    componentDidMount() {
        this.fetchData();
    }

    onError(message) {
        this.props.onError(message);
    }

    fetchData() {
        let pathId = this.props.match.params['id'];
        let path = 'api/profTest/' + pathId + '/info';
        
        GetFetch<any>(path)
            .then(data => {
                this.setState({
                    id: data.id,
                    name: data.name,
                    description: data.description,
                    approximateTime: data.approximateTime,
                    questionsCount: data.questionsCount,
                    loading: false
                });
            })
            .catch(er => this.props.onError(er))
    }

    private renderTestInfo() {
        return <div className="pad-for-footer">
            <div className="jumbotron jumbotron-fluid">
                <div className="container">
                    <h1 className="display-1">{this.state.name}</h1>
                    <p>
                        <Link to={'/quiz/' + this.state.id} className="btn btn-primary">Розпочати тест</Link>
                    </p>
                </div>
            </div>
            <section className="container-fluid">
                <div className="container">
                    <p className="text-left text-muted h4">Код Тесту: {this.state.id}</p>
                    <p className="text-left text-muted h4">Час на виконання: {this.state.approximateTime} </p>
                    <p className="text-left text-muted h4">Кількість питань: {this.state.questionsCount}</p>
                    <p className="text-left text-muted h4">Опис: {this.state.description} </p>
                </div>
            </section>
        </div>
    }

    public render() {
        return this.state.loading ?
            <Loading /> :
            this.renderTestInfo();


    }
}


