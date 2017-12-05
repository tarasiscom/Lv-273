import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
import { Loading } from './Loading';
import TestResults from './TestResult';

interface TestResult {
    generalDir: GeneralDir;
    score: number;
}

interface GeneralDir {
    id: number;
    name: string;
    description: string;
}

interface GeneralDirectionResult {
    testresult: TestResult[];
    loading: boolean;
}

export class SavedTestResult extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, GeneralDirectionResult> {
    constructor() {
        super();
        this.state = {
            loading: true,
            testresult: []
        }
    }

    componentDidMount() {
        GetFetch<any>('api/User/GetTestResult/' + this.props.match.params["id"])
            .then(data => this.setState({ testresult: data, loading: false }))
            .catch(err => this.props.onError(err))
    }

    public render() {
        return <div className="pad-for-footer">
            {this.state.loading ?
                <Loading />
                :
                <TestResults testresult={this.state.testresult} onError={this.props.onError} />}
            </div>
    }
}
