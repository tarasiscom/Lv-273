import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

import { ErrorHandlerProp, GetFetch } from './App';

interface NavState{
    isAuthenticated: boolean;
}

export class NavigationMenu extends React.Component<{}, NavState> {
    constructor() {
        super();
        this.state = {
            isAuthenticated: false
        }
        
    }

    componentWillMount() {
        GetFetch<any>('api/CheckAuth')
            .then(data => {
                if (data != this.state.isAuthenticated) {
                    this.setState({ isAuthenticated: data })
                }
            });
    }




    public render() {
        

        return <div>
            <nav className="navbar navbar-inverse">
                <div className="container-fluid ">
                    <div className="navbar-header">
                        <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span className="icon-bar"> </span>
                            <span className="icon-bar"> </span>
                            <span className="icon-bar"> </span>
                        </button>
                        <Link className="navbar-brand" to={'/'}> EPA</Link>
                    </div>

                    <div className="navbar-collapse collapse">
                        <ul className="nav navbar-nav">
                            <li><Link to={'/'}>Усі Університети</Link></li>
                            <li><Link to={'/profTest'}>Профорієнтаційні Тести</Link></li>
                            <li><Link to={'/ChooseSpecialty'}>Обрати Спеціальність</Link></li>
                        </ul>

                        {
                            !this.state.isAuthenticated ?
                                <ul className="nav navbar-nav navbar-right">
                                    <li><Link to={'/Login'}>Вхід</Link></li>
                                    <li><Link to={'/Registration'}>Реєстрація</Link></li>
                                </ul>

                                :

                                <ul className="nav navbar-nav navbar-right">
                                    <li><a href ='/account/Logout'>Вихід</a></li>
                                    <li><Link to={'/PersonalCabinet'}>Персональний кабінет</Link></li>
                                </ul>
                        }
                        
                    </div>
                </div>
            </nav>
        </div>
    }
}