import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface FetchDataExampleState {
    dates: DateAPI[];
    loading: boolean;
}

interface DateAPI {
    id: number;
    dt: string;
}

export class FetchData extends React.Component<RouteComponentProps<{}>, FetchDataExampleState> {
    constructor() {
        super();
        this.state = { dates: [], loading: true };

        fetch('api/SampleData/GetDates')
            .then(response => response.json() as Promise<DateAPI[]>)
            .then(data => {
                this.setState({ dates: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderDatesTable(this.state.dates);

        return <div>
            <h1>Dates DataBase Table</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>;
    }

    private static renderDatesTable(dates: DateAPI[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>id</th>
                    <th>date</th>

                </tr>
            </thead>
            <tbody>
                {dates.map(dates =>
                    <tr key={dates.id}>
                        <td>{dates.id}</td>
                        <td>{dates.dt}</td>
                    </tr>
                )}
            </tbody>
        </table>;
    }
}

