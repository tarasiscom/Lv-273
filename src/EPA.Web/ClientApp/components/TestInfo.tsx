import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';


interface TestDetailInformation {
    id: number;
    name: string;
    description: string;
    approximatedtime: number;
    questioncount: number;
    loading: boolean;

}

export class TestInfo extends React.Component<RouteComponentProps<{}>, TestDetailInformation> {
    constructor() {
        super();
        this.state = { loading: true, id: 0, name: "", description: "", approximatedtime: 0, questioncount:0, };
    }

    public render() {
        return <div>
           
        </div>;
    }

    
}
