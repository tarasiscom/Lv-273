import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Crypto from 'crypto-js';

interface User {
    firstName: string;
    lastName: string;
    middleName: string;
    email: string;
    password: string;
    confirmPassword: string;
}

export class Registration extends React.Component<RouteComponentProps<{}> & ErrorHandlerProp, User >
{
    constructor(props) {

        super(props);
        this.state = {
            firstName: "",
            lastName: "",
            middleName: "",
            email: "",
            password: "",
            confirmPassword: ""
        }
    }

    sendData = () => {
        if (this.state.password != this.state.confirmPassword) {
            alert('Підтвердження паролю не співпадає');

        }
        else {
            let hash = Crypto.SHA512(this.state.password);
            
            let userInfo = {
                lastname: this.state.lastName,
                firstName: this.state.firstName,
                middleName: this.state.middleName,
                email: this.state.email,
                passwordHash: this.state.password
            }

            alert();

            fetch('api/registration', {
                method: 'POST',
                body: JSON.stringify(userInfo),
                headers: { 'Content-Type': 'application/json' }
            })
               
        }
    }
            

    /*
    handleChange(event)
    {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({[name]: value })
    }
    */

    render() {
        return <div className="registration">
            <form role="form" onSubmit={this.sendData}>
                <div className="input-group">
                    <input type="text" name="lastName" pattern="^([^\u0000-\u007F]|[ -]|[Aa-Zz])+$" className="form-control" placeholder="Прізвище" required
                        value={this.state.lastName} onChange={(event) => this.setState({ lastName: event.target.value })}
                        title="Поле може містити літери, відступи і дефіс"></input>
                </div>
                <div className="input-group">
                    <input type="text" name="firstName" pattern="^([^\u0000-\u007F]|[ -]|[Aa-Zz])+$" className="form-control" placeholder="Імя" required
                        value={this.state.firstName} onChange={(event) => this.setState({ firstName: event.target.value })}
                        title="Поле може містити літери, відступи і дефіс"></input>
                </div>
                <div className="input-group">
                    <input type="text" name="middleName" pattern="^([^\u0000-\u007F]|[ -]|[Aa-Zz])+$" className="form-control" placeholder="По батькові" 
                        value={this.state.middleName} onChange={(event) => this.setState({ middleName: event.target.value })}
                        title="Поле може містити літери, відступи і дефіс"></input>
                </div>
                <div className="input-group">
                    <input type="email" name="email"  className="form-control" id="inputEmail" placeholder="Email" required
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
                    <button type="submit" className="btn btn-primary" >Відправити</button>
                </div>               
            </form>
        </div>
    }
}