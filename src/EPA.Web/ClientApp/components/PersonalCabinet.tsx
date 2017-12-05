﻿import * as React from 'react';
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
    firstName: string;
    surname: string;
    email: string;
    phone: string;
    district: string;
}

export class PersonalCabinet extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, StateTypes> {

    constructor() {
        super();
        this.state = {
            userInfo: { firstName: "", surname: "", email: "", phone: "", district: "" },
            loading: true
        }
    }

    componentDidMount() {
        this.fetchUserPersonalInformation();
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
                    {this.renderUsersPreference()}
                    {this.renderUserPersonalInformation()}
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

    private renderUsersPreference() {

        return <div>
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


    }

    private renderUserPersonalInformation() {
        return <div className="personal-info">
            <h3 className="text-center">Персональна інформація</h3>
            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">Ім'я </p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">{this.state.userInfo.firstName}</p>
                <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary "> Change</button>
            </div>
            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">Прізвище </p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">{this.state.userInfo.surname}</p>
                <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary"> Change</button>
            </div>
            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">e-mail </p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">{this.state.userInfo.email}</p>
                <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary"> Change</button>
            </div>

            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">Область </p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">{this.state.userInfo.district}</p>
                {this.renderButtonForDistrict()}
            </div>
            
            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">Телефон </p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">{this.state.userInfo.phone}</p>
                {this.renderButtonForPhone()}
            </div>
        </div>


    }

    private fetchUserPersonalInformation() {
        let path = 'api/User/GetUserPersonalInformation';

        GetFetch<any>(path)
            .then(data => {
                this.setState(
                    {
                        userInfo: data,
                        loading: false
                    })
            }).catch(er => this.props.onError(er))
        
    }

    private renderButtonForDistrict()
    {
        
        if (this.state.userInfo.district == null) {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary">Додати</button>
        }
        else {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary">Змінити</button>
            
        }
    }

    private renderButtonForPhone() {

        if (this.state.userInfo.phone == null) {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary">Додати</button>
        }
        else {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary">Змінити</button>

        }
    }
}