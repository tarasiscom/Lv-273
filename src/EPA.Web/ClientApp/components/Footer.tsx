import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class Footer extends React.Component<{}, {}> {
    public render() {
        return <footer className="navbar-fixed-bottom row-fluid">
            <div className="navbar-inner">
                <div className="container">
                    <h2 className="text-center"> THIS IS FOOTER</h2>
                </div>
            </div>
        </footer>

    }
}