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
        let hash = Crypto.SHA512(this.state.password);
        /*
            let userInfo = {
                email: this.state.email,
                password: this.state.password,
                lastname: "",
                firstName: "",
                middleName: ""
            }
        
            fetch('api/Registration/sendU', {
                method: 'POST',
                body: JSON.stringify(userInfo),
                headers: { 'Content-Type': 'application/json' }
            })*/

            fetch('api/Login/' + this.state.email + "/" + hash)
        }

    render() {

        return <div className="registration">
            <form role="form">
                
            <div className="input-group">
                    <input type="email" name="email" className="form-control" id="inputEmail" placeholder="Email" data-error="Дана електонна пошта недійсна" required
                        onChange={(event) => this.setState({ email: event.target.value })}></input>
            </div>
            <div className="input-group">
                    <input type="password" name="password" data-minlength="6" className="form-control" id="inputPassword" placeholder="Пароль" required
                        onChange={(event) => this.setState({ password: event.target.value })}></input>
            </div>
            <div className="form-group userSubmit">
                    <button type="submit" className="btn btn-primary cus-margin" onClick={this.sendData}>Вхід</button>
            </div>               
            </form>
            <div id = "passToReg">
                <span>Не маєте облікового запису? </span>
                <Link to={'/Registration'}>Реєстрація</Link>
            </div>
        </div>
    }
}