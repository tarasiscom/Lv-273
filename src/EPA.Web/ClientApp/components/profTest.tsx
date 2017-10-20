import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { Link, NavLink } from 'react-router-dom';

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
    constructor()
    {
        super();
        this.state = { loading: true, tests :[] }
        fetch("api/profTest/list/GetTests")
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
            <h1>Professional Tests</h1>
            <p>Here you can see a list of professional tests</p>
            {contents}
        </div>;
    }

    private static renderTestsList(tests: DataAPI[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>id</th>
                    <th>name</th>

                </tr>
            </thead>
            <tbody>
                {tests.map(tests=>
                    <tr key={tests.id}>
                        <td>{tests.id}</td>
                        <td>{tests.name}</td>
                    </tr>
                )}
            </tbody>
        </table>;

        /*<div>
            {tests.map(tests => <p key={tests.Id}> # {tests.Id} {tests.Name} <Link to={'/profTest/${tests.Id}'}>;
                Детальніша інформація
                </Link>
            </p>)}
        </div>//*/

    }
}

