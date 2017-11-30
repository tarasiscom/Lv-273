import * as React from 'react';
import { RouteComponentProps, withRouter, Switch, Redirect } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';


export class ConfirmEmail extends React.Component<RouteComponentProps<{}>, {}> {
    constructor() {
        super();
    }

    render() {
            return <div className="pad-for-footer">
                <div className="jumbotron jumbotron-fluid">
                    <div className="container">
                        <h2 className="display-1">Обліковий запис успішно підтвердженно</h2>
                        <p>
                            <Link to={'/Login'} className="btn btn-primary">Розпочати</Link>
                        </p>
                    </div>
                </div>
            </div>
    }
}