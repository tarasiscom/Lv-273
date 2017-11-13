import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class NavigationMenu extends React.Component<{}, {}> {
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
                        <ul className="nav navbar-nav navbar-right">
                            <li><Link to={'/'}>Вхід</Link></li>
                            <li><Link to={'/'}>Реєстрація</Link></li>
                        </ul>
                    </div>
                </div>
            </nav>
        </div>
    }
}