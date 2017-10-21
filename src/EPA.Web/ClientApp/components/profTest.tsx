import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { Link, NavLink ,BrowserRouter as Router,
  Route } from 'react-router-dom';




interface TestsDataState {
    tests: DataAPI[];
    loading: boolean;
}
interface DataAPI
{
    id: number;
    name: string;
}


export class ProfTest extends React.Component<RouteComponentProps<{}>, TestsDataState> {
    constructor(){
        super();
        this.state = { tests : [], loading: true  }
        fetch('api/profTest/list')
            .then(response => response.json() as Promise<DataAPI[]>)
            .then(data => {
                this.setState({ tests: data, loading: false  });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : ProfTest.renderTestsList(this.state.tests);

        return <div>
            <h1 className="text-center">Профорієнтаційні тести</h1>
            <p className="text-center">Тут Ви можете переглянути список тестів</p>
            {contents}
        </div>;
    }

    private static renderTestsList(tests: DataAPI[]) {
        return  <div>
            <table className="table">
            <thead>
                <tr>
                    <th>№</th>
                    <th>Ім'я</th>
                </tr>
            </thead>
            <tbody>
                {tests.map(tests =>
                    <tr key={tests.id}>
                            <td>{tests.id}</td>
                            <td><Link to={'/testInfo/${tests.id}'} > {tests.name}</Link></td>
                            
                    </tr>
                )}
            </tbody>
        </table>
        </div>

    }
}

//