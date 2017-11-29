import * as React from 'react';
import { RouteComponentProps, withRouter, Switch, Redirect } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import { ErrorHandlerProp, ResponseChecker } from './App';
import { Loading } from './Loading';



interface StateTypes {
    loading: boolean
}

export class ConfirmEmail extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {
    constructor() {
        super();
        this.state = {
            loading: true
        }
    }

    render() {
        if (this.state.loading) {
            return <Loading />
        }
        else {
            return <div className="pad-for-footer">
                <div className="jumbotron jumbotron-fluid">
                    <div className="container">
                        <h1 className="display-1">Обліковий запис успішно підтвердженно</h1>
                        <p>
                            <Link to={'/Login'} className="btn btn-primary">Розпочати</Link>
                        </p>
                    </div>
                </div>
            </div>
        }
    }
}