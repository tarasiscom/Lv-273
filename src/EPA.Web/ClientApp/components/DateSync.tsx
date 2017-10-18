import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface DateSyncState {
    lastdate: Date;
    loading: boolean;
}

export class DateSync extends React.Component<RouteComponentProps<{}>, DateSyncState> {
    constructor() {
        super();
        this.state = { lastdate: new Date(Date.now), loading: true };

        fetch('api/Date/GetDate')
            .then(response => response.json() as Promise<Date>)
            .then(data => {
                this.setState({ lastdate: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : DateSync.renderDatesTable(this.state.lastdate);

        return <div>
            <h1>Dates DataBase Table</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>;
    }

    private static renderDatesTable(dates: Date) {
        return <div>
            {dates}
        </div>;
    }
}


