import * as React from 'react';
import { RouteComponentProps, Redirect} from 'react-router';
import Crypto from 'crypto-js';
import { ErrorHandlerProp, GetFetch, PostFetch } from './App';


interface User {
    firstName: string;
    lastName: string;
    middleName: string;
    email: string;
    password: string;
    confirmPassword: string;
    error: string;
}

interface Status
{
    statusCode: number;
    message: string;
}


export class Registration extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, User>
{
    constructor(props) {

        super(props);
        this.state = {
            firstName: "",
            lastName: "",
            middleName: "",
            email: "",
            password: "",
            confirmPassword: "",
            error: ""
        }
    }

    sendData() {
        let hash = Crypto.SHA512(this.state.password);

        let userInfo = {
            firstName: this.state.firstName,
            surname: this.state.lastName,
            middleName: this.state.middleName,
            email: this.state.email,
            passwordHash: this.state.password,
            userName: this.state.email
        }

        PostFetch<Status>('api/registration', userInfo)
            .then(data =>
            {
                    this.setState({ error: "Реєстрація пройшла успішно. Перевірте електронну пошту." });
            }).catch(error => this.setState({ error: "Реєстрація невдала." }))
    }

    validate = () => {
        let name = new RegExp("^([^\u0000-\u007F]|[ -]|[A-Za-z])+$");
        let middleName = new RegExp("^([^\u0000-\u007F]|[ -]|[A-Za-z])*$");
        let email = new RegExp("^(?=.*[@]{1}).{5,}$");
        let password = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,}$");
        if (!name.test(this.state.lastName)) {
            this.setState({ error: "Прізвище введено невірно. Поле може містити літери, відступи і дефіс" });
            return;
        }
        if (!name.test(this.state.firstName)) {
            this.setState({ error: "Імя введено невірно. Поле може містити літери, відступи і дефіс" });
            return;
        }
        if (!middleName.test(this.state.middleName)) {
            this.setState({ error: "По батькові введено невірно. Поле може містити літери, відступи і дефіс" });
            return;
        }
        if (!email.test(this.state.email)) {
            this.setState({ error: "Електрнна пошта повинна містити символ @" });
            return;
        }
        if (!password.test(this.state.password)) {
            this.setState({ error: "Пароль введено невіно. Пароль повинен  містити цифру, велику і малу латинські літери та мати довжину більше 5 символів" });
            return;
        }
        if (!password.test(this.state.confirmPassword)) {
            this.setState({ error: "ПІдтвердження паролю введено невірно. Пароль повинен  містити цифру, велику і малу латинські літери та мати довжину більше 5 символів6" });
            return;
        }

        if (this.state.password != this.state.confirmPassword) {
            this.setState({ error: 'Підтвердження паролю ' });
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
                    <input type="text" name="lastName" pattern="^([^\u0000-\u007F]|[ -]|[A-Za-z])+$" className="form-control" placeholder="Прізвище" required
                        value={this.state.lastName} onChange={(event) => this.setState({ lastName: event.target.value })}
                        title="Поле може містити літери, відступи і дефіс"></input>
                </div>
                <div className="input-group">
                    <input type="text" name="firstName" pattern="^([^\u0000-\u007F]|[ -]|[A-Za-z])+$" className="form-control" placeholder="Імя" required
                        value={this.state.firstName} onChange={(event) => this.setState({ firstName: event.target.value })}
                        title="Поле може містити літери, відступи і дефіс"></input>
                </div>
                <div className="input-group">
                    <input type="text" name="middleName" pattern="^([^\u0000-\u007F]|[ -]|[A-Za-z])+$" className="form-control" placeholder="По батькові"
                        value={this.state.middleName} onChange={(event) => this.setState({ middleName: event.target.value })}
                        title="Поле може містити літери, відступи і дефіс"></input>
                </div>
                <div className="input-group">
                    <input type="email" name="email" className="form-control" id="inputEmail" placeholder="Email" required
                        value={this.state.email} onChange={(event) => this.setState({ email: event.target.value })}></input>
                </div>
                <div className="input-group">
                    <input type="password" name="password" pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,}$" className="form-control" id="inputPassword" placeholder="Пароль" required
                        value={this.state.password} onChange={(event) => this.setState({ password: event.target.value })}
                        title="Пароль повинен  містити цифру, велику і малу латинські літери та мати довжину більше 5 символів"></input>
                </div>
                <div className="input-group">
                    <input type="password" name="password" pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,}$" className="form-control" placeholder="Підтвердити пароль" required
                        value={this.state.confirmPassword} onChange={(event) => this.setState({ confirmPassword: event.target.value })}></input>
                </div>
                <div className="form-group userSubmit">
                    <button className="btn btn-primary" onClick={this.validate}>Відправити</button>
                </div>
                <p id="errorMsg"> {this.state.error} </p>
            </div>
        </div>
    }
}

