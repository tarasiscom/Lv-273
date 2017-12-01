import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
import { Loading } from './Loading';

interface StateTypes {
    userInfo: User;
    loading: boolean;
}

interface User {
    name: string;
    email: string;
    phone: string;
}

export class PersonalCabinet extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {

    constructor() {
        super();
        this.state = {
            userInfo: { name: "", email: "", phone: "" },
            loading: false
        }
    }

    componentDidMount() {

    }

    render() {
        if (this.state.loading) {
            return <Loading />
        }
        else {
            return <div className="pad-for-footer">
                {this.renderPersonalCabinet()}
            </div>
        }
    }

    private renderPersonalCabinet() {
        let pageSkeleton;
        pageSkeleton = < main className="container " >

            <header className="text-center">
                <h1 className="padding-personal-cabinet">Особистий кабінет</h1>
            </header>

            <div className="row">
                <section className="col-md-9 col-lg-9 col-sm-12 col-xs-12 container-fluid">

                    <div >
                        <div className="panel-group">

                            <div className="panel panel-default panel-scale">
                                <div className="panel-heading">
                                    <h4 className="panel-title">
                                        <a data-toggle="collapse" href="#collapse1">Результати тестів</a>
                                    </h4>
                                </div>
                                <div id="collapse1" className="panel-collapse collapse">
                                    <div className="panel-body">Тут будуть результати тестів...</div>
                                    <div className="panel-body">Тест 1</div>
                                    <div className="panel-body">Тест 2</div>

                                </div>
                            </div>


                            <div className="panel panel-default panel-scale">
                                <div className="panel-heading">
                                    <h4 className="panel-title">
                                        <Link to={'/FavoriteSpecialties'}>Обрані Спеціальності</Link>
                                    </h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

                <aside className=" col-md-3 col-lg-3 col-sm-12 col-xs-12 container-fluid text-center">
                    <img src="http://vaz.od.ua/assets/blank_avatar-b870b9bb4856dbcb4ed86cfed0349975.png" alt="User Logo" className="rounded img-thumbnail user-logo" />
                </aside>

            </div>
        </main>

        return <div>
            {pageSkeleton}
        </div>
    }

}