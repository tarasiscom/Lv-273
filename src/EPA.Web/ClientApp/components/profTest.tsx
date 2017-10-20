import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface TestsDataState {
    tests: DataAPI[];
    loading: boolean;
}
interface DataAPI
{
    Id: number;
    Name: string;
}


export class ProfTest extends React.Component<RouteComponentProps<{}>, TestsDataState> {
    constructor()
    {
        super();
        this.state = { loading: true, tests :[] }
        fetch("api/profTest/list/GetTests")
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
            <h1>Professional Tests</h1>
            <p>Here you can see a list of professional tests</p>
            {contents}
        </div>;
    }

    private static renderTestsList(tests: DataAPI[]) {
        return <div>
            {tests.map(tests => <p key={tests.Id}> № {tests.Id} {tests.Name} ;</p>)}
        </div>

    }
}

