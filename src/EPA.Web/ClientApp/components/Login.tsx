﻿import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import Crypto from 'crypto-js';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';

interface LoginInfo {
    email: string;
    password: string;
    error: string;
}

interface Status {
    statusCode: number;
    message: string
}

export class Login extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, LoginInfo>
{
    constructor() {

        super();
        this.state = {
            email: "",
            password: "",
            error: ""
        }
    }

    sendData = () => {
        let hash = Crypto.SHA512(this.state.password);
        
        let loginInfo = {
            email: this.state.email,
            password: this.state.password
        }

        PostFetch<Status>('api/login', loginInfo)
            .then(data => {
                this.props.history.push('/PersonalCabinet');
            }).catch(error => this.setState({ error: "Електронна пошта, або пароль введені невірно." }))
        
    }

    validate = () => {
        let email = new RegExp("^(?=.*[@]{1}).{5,}$");
        let password = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,}$");
       
        if (!email.test(this.state.email)) {
            this.setState({ error: "Електрнна пошта повинна містити символ @" });
            return;
        }
        if (!password.test(this.state.password)) {
            this.setState({ error: "Пароль введено невіно. Пароль повинен  містити цифру, велику і малу латинські літери та мати довжину більше 5 символів" });
            return;
        }
        else {
            this.sendData();
        }
    }

    render() {
        return <div className="registration">
            <div>
                <div className="input-group">
                    <input type="email" name="email" className="form-control" id="inputEmail" placeholder="Email" required
                        onChange={(event) => this.setState({ email: event.target.value })}></input>
                </div>
                <div className="input-group">
                    <input type="password" name="password" pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,}$" className="form-control" id="inputPassword" placeholder="Пароль" required
                        onChange={(event) => this.setState({ password: event.target.value })}></input>
                </div>
                <div className="form-group userSubmit">
                    <button type="submit" id="enter" className="btn btn-primary cus-margin" onClick={this.sendData}>Вхід</button>
                </div>
            </div>
            <div id="passToReg">
                <span>Не маєте облікового запису? </span>
                <Link to={'/Registration'}>Реєстрація</Link>
            </div>
            <p id="errorMsg">{this.state.error}</p>
        </div>
    }
}

