import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';
import {
    Link, NavLink, BrowserRouter as Router,
    Route
} from 'react-router-dom';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';
import { Loading } from './Loading';
import ReactModal from 'react-modal';
import Crypto from 'crypto-js';

const customStyles = {
    content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        width: '530px',
        height: '320px'
    }
};
interface ChangePassword {
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
    
}

interface StateTypes {
    userInfo: User;
    loading: boolean;
    modalIsOpen: boolean;
    changePassword: ChangePassword;
    message: string;
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
            loading: true,
            modalIsOpen: false,
            changePassword: { oldPassword: "", newPassword: "", confirmPassword: "" },
            message: ""
        }

        this.openModal = this.openModal.bind(this);
        this.closeModal = this.closeModal.bind(this);
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
                    {this.renderUserPersonalInformation()}
                    {this.renderUsersPreference()}
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
            <h3 className="text-left personal-info-header">
                Персональна інформація</h3>
            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">Ім'я </p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">{this.state.userInfo.firstName} {this.state.userInfo.surname} </p>
                <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary glyphicon glyphicon-pencil " disabled></button>
            </div>

            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">e-mail </p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">{this.state.userInfo.email}</p>
                <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary glyphicon glyphicon-pencil " disabled></button>
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

            <div className="row personal-info-row">
                <p className="col-md-3 col-lg-3 col-sm-3 col-xs-3">Пароль</p>
                <p className="col-md-5 col-lg-5 col-sm-5 col-xs-5">**********</p>
                <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary" onClick={this.openModal}> Змінити</button>
            </div>
            {this.renderModalForm()}
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

    private ChangePassword()
    {
        let hashOldPassword = Crypto.SHA512(this.state.changePassword.oldPassword);
        let hashNewPassword = Crypto.SHA512(this.state.changePassword.newPassword);

        if (this.state.changePassword.confirmPassword == this.state.changePassword.newPassword) {
            let path = 'api/User/ChangePassword';
            let passwordInfo = {
                oldPassword: hashOldPassword.toString(Crypto.enc.Base64),
                newPassword: hashNewPassword.toString(Crypto.enc.Base64)
            }

            PostFetch<any>(path, passwordInfo)
                .then(data => {
                    this.setState(
                        {
                            message:data
                        })
                })
                .catch(er => this.props.onError(er))
            
        }
        else {
            this.setState({
                message:"Не правильне підтвердження паролю."
            });
            }
    }

    private renderButtonForDistrict()
    {
        
        if (this.state.userInfo.district == null) {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary" disabled>Додати</button>
        }
        else {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary glyphicon glyphicon-pencil " disabled></button>
            
        }
    }

    private renderButtonForPhone() {

        if (this.state.userInfo.phone == null) {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary" disabled>Додати</button>
        }
        else {
            return <button type="button" className="col-md-2 col-lg-2 col-sm-2 col-xs-12 btn btn-secondary glyphicon glyphicon-pencil " disabled></button>

        }
    }

    private renderModalForm()
    {
        return <div>
            <ReactModal
                isOpen={this.state.modalIsOpen}
                onRequestClose={this.closeModal}
                style={customStyles}
                contentLabel="Example Modal"
            >
                <h2 className="personal-info-row">Зміна паролю</h2>
                <div className="input-group personal-info-row">
                    <span className="input-group-addon"><i className="glyphicon glyphicon-lock"></i></span>
                    <input id="old-password" type="password" className="form-control" pattern="^.{6,}$"
                        name="password" placeholder="Старий пароль" required onChange={(event) =>
                            this.setState({
                                changePassword: {
                                    oldPassword: event.target.value,
                                    newPassword: this.state.changePassword.newPassword,
                                    confirmPassword: this.state.changePassword.confirmPassword
                                }
                            })}></input>
                </div>

                <div className="input-group personal-info-row">
                    <span className="input-group-addon"><i className="glyphicon glyphicon-lock"></i></span>
                    <input id="new-password" type="password" className="form-control" pattern="^.{6,}$"
                        name="new-password" placeholder="Новий пароль" required onChange={(event) =>
                            this.setState({
                                changePassword: {
                                    oldPassword: this.state.changePassword.oldPassword,
                                    newPassword: event.target.value,
                                    confirmPassword: this.state.changePassword.confirmPassword
                                }
                            })}></input>
                </div>

                <div className="input-group personal-info-row">
                    <span className="input-group-addon"><i className="glyphicon glyphicon-lock"></i></span>
                    <input id="confirm-new-password" type="password" className="form-control" pattern="^.{6,}$"
                        name="confirm-new-password" placeholder="Підтвердіть новий пароль" required onChange={(event) =>
                            this.setState({
                                changePassword: {
                                    oldPassword: this.state.changePassword.oldPassword,
                                    newPassword: this.state.changePassword.newPassword,
                                    confirmPassword: event.target.value
                                }
                            })}></input>
                </div>

                {this.resultChangePasswordOutput()}

                <div className="text-right">
                    <button className="btn" onClick={this.handleChangePassword}>Підтвердити</button>
                </div>
            </ReactModal>
        </div>
    }

    private handleChangePassword = () => {
        this.ChangePassword();
    }

    private resultChangePasswordOutput()
    {
        if (this.state.message == "") {
            return <div></div>
        }
        else {
            return <div >
                <span className="h5 text-center">{this.state.message}</span>
            </div>
        }
    }

    openModal() {
        this.setState({ modalIsOpen: true });
    }

    closeModal() {
        this.setState({
            modalIsOpen: false,
            message: ""
        });
        
    }

}