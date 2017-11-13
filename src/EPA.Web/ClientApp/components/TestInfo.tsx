import * as React from 'react';
import { RouteComponentProps, withRouter, Switch, Redirect } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import PropTypes from 'prop-types';

//import { Error404inComp } from './errors/404';

interface TestDetailInformation {
    id: number;
    name: string;
    description: string;
    approximateTime: number;
    questionsCount: number;
    loading: boolean;
}
interface myProps {
    onError: PropTypes.func    
}

export class TestInfo extends React.Component<RouteComponentProps<{}>&myProps, TestDetailInformation> {
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
        fetch(path)
            .then(response => response.ok ? response.json() as Promise<TestDetailInformation> : this.props.onError(response.status.toString()) )
            .then(data => {
                this.setState({ id: data.id, name: data.name, description: data.description, approximateTime: data.approximateTime, questionsCount: data.questionsCount, loading: false });
            })
    }

    private renderTestInfo()
    {
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
        let content = this.state.loading ?
            <p>Loading...</p>:
            this.renderTestInfo();

        return content;
    }
}


