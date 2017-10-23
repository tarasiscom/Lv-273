import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { TestInfo } from './TestInfo'
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';




interface TestsDataState {
    tests: DataAPI[];
    loading: boolean;
}
interface DataAPI {
    id: number;
    name: string;
}



export class ProfTest extends React.Component<RouteComponentProps<{}>, TestsDataState> {
    constructor() {
        super();
        this.state = { tests: [], loading: true }
        fetch('api/profTest/list')
            .then(response => response.json() as Promise<DataAPI[]>)
            .then(data => {
                this.setState({ tests: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ProfTest.renderTestsList(this.state.tests);

        return <div>
            <section className="jumbotron text-center">
                <div className="container">
                    <h1 className="jumbotron-heading">Профорієнтаційні тести</h1>
                    <p className="lead text-muted">Тут Ви можете переглянути список тестів</p>
                </div>
            </section>
            {contents}
        </div>;
       
    }

    private static renderTestsList(tests: DataAPI[]) {
        return <div className="container">
            <table className="table table-striped table table-hover table-sm">
                <thead className="thread-dark">
                    <tr>
                        <th className="text-center" scope="col">Номер Тесту</th>
                        <th className="text-center" scope="col">Назва тесту</th>
                        <th className="text-center" scope="col">Переглянути тест</th>
                    </tr>
                </thead>
                <tbody>
                    {tests.map(tests =>
                        <tr key={tests.id}>
                            <td className="text-center">{tests.id}</td>
                            <td className="text-center"> {tests.name}</td>
                            <td className="text-center"><Link to={'/testInfo/' + tests.id} ><span className="glyphicon glyphicon-list-alt"></span></Link></td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>

    }
}
