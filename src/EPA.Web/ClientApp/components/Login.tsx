import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import Crypto from 'crypto-js';

interface UserInfo {
    email: string;
    password: string
}

export class Login extends React.Component<RouteComponentProps<{}>, UserInfo>
{
    constructor() {

        super();
        this.state = {
            email: "",
            password: ""
        }
    }

    sendData = () => {
        var hash = Crypto.MD5(this.state.password);
        
            let userInfo = {
                email: this.state.email,
                password: hash.toString(Crypto.enc.Base64),
                lastname: "",
                firstName: "",
                middleName: ""
            }

            fetch('api/Login/send', {
                method: 'POST',
                body: JSON.stringify(userInfo),
                headers: { 'Content-Type': 'application/json' }
            })
        }

    render() {

        return <div className="registration">
            <form role="form" onSubmit={this.sendData}>
                
            <div className="input-group">
                    <input type="email" name="email" className="form-control" id="inputEmail" placeholder="Email" required
                        onChange={(event) => this.setState({ email: event.target.value })}></input>
            </div>
            <div className="input-group">
                    <input type="password" name="password" pattern="^.{6,}$" className="form-control" id="inputPassword" placeholder="Пароль" required
                        onChange={(event) => this.setState({ password: event.target.value })}></input>
            </div>
            <div className="form-group userSubmit">
                    <button type="submit" className="btn btn-primary cus-margin">Вхід</button>
            </div>               
            </form>
            <div id = "passToReg">
                <span>Не маєте облікового запису? </span>
                <Link to={'/Registration'}>Реєстрація</Link>
            </div>
        </div>
    }
}