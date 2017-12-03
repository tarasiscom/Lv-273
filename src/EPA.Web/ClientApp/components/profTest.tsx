﻿import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import { ErrorHandlerProp, GetFetch } from './App';
import { Loading } from './Loading';

interface TestsDataState {
    tests: DataAPI[];
    loading: boolean;
}

interface DataAPI {
    id: number;
    name: string;
}

export class ProfTest extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, TestsDataState> {
    constructor() {
        super();
        this.state = { tests: [], loading: true };
    }

    componentDidMount() {

        GetFetch<any>('api/profTest/list')
            .then(data => {
                this.setState({ tests: data, loading: false });
            })
            .catch(er => this.props.onError(er))
    }


    public render() {
        return this.state.loading
            ? <Loading />
            : this.renderTestsList();
        
    }

    private renderTestsList() {

        return (<div className="pad-for-footer">
            <section className="jumbotron text-center">
                <div className="container">
                    <h1 className="jumbotron-heading">Профорієнтаційні тести</h1>
                    <p className="lead text-muted">Тут Ви можете переглянути список тестів</p>
                </div>
            </section>
            <div className="container">
                <table className="table table-striped table table-hover table-sm">
                    <thead className="thread-dark">
                        <tr>
                            <th className="text-center" scope="col">Номер Тесту</th>
                            <th className="text-center" scope="col">Назва тесту</th>
                            <th className="text-center" scope="col">Переглянути тест</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.tests.map((tests, id) =>
                            <tr key={id}>
                                <td className="text-center">{id + 1}</td>
                                <td className="text-center"> {tests.name}</td>
                                <td className="text-center">
                                    <Link to={'/testInfo/' + tests.id} >
                                        <span className="glyphicon glyphicon-list-alt"></span>
                                    </Link>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>);
    }
}
