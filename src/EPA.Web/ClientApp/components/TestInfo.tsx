import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';


interface TestDetailInformation {
    Id: number;
    Name: string;
    Description: string;
    ApproximatedTime: number;
    QuestionCount: number;
    loading: boolean;

}

export class TestInfo extends React.Component<RouteComponentProps<{}>, TestDetailInformation> {
    constructor() {
        super();
        this.state = { loading: true, Id: 0, Name: "", Description: "", ApproximatedTime: 0, QuestionCount:0, };
    }

    public render() {
        return <div>
           
        </div>;
    }

    
}
