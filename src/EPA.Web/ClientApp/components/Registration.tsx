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
    msg: string;
    error: string;
}

interface Result
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
            msg: "",
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

        PostFetch<any>('api/registration', userInfo)
            .then(data =>
            {
                this.setState({ msg: "Реєстрація пройшла успішно. Перевірте електронну пошту."});
            }).catch(error => this.setState({ msg: "Реєстрація невдала." }))
    }

    validate = () => {
        let name = new RegExp("^([^\u0000-\u007F]|[ -]|[A-Za-z])+$");
        let middleName = new RegExp("^([^\u0000-\u007F]|[ -]|[A-Za-z])*$");
        let email = new RegExp("^(?=.*[@]{1}).{5,}$");
        let password = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,}$");
        if (!name.test(this.state.lastName)) {
            this.setState({ msg: "Прізвище введено невірно. Поле може містити літери, відступи і дефіс" });
            return;
        }
        if (!name.test(this.state.firstName)) {
            this.setState({ msg: "Імя введено невірно. Поле може містити літери, відступи і дефіс" });
            return;
        }
        if (!middleName.test(this.state.middleName)) {
            this.setState({ msg: "По батькові введено невірно. Поле може містити літери, відступи і дефіс" });
            return;
        }
        if (!email.test(this.state.email)) {
            this.setState({ msg: "Електрнна пошта повинна містити символ @" });
            return;
        }
        if (!password.test(this.state.password)) {
            this.setState({ msg: "Пароль введено невіно. Пароль повинен  містити цифру, велику і малу латинські літери та мати довжину більше 5 символів" });
            return;
        }
        if (!password.test(this.state.confirmPassword)) {
            this.setState({ msg: "ПІдтвердження паролю введено невірно. Пароль повинен  містити цифру, велику і малу латинські літери та мати довжину більше 5 символів6" });
            return;
        }

        if (this.state.password != this.state.confirmPassword) {
            this.setState({ msg: 'Підтвердження паролю введено невірно. Підтвердження паролю не співпадає з введеним паролем.' });
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
                <p id="errorMsg"> {this.state.msg} </p>
            </div>
        </div>
    }
}

