import * as React from 'react';
import { Link, NavLink, RouteComponentProps } from 'react-router-dom';

interface Err {
    message: string;
}

export class Error404 extends React.Component<Err, {}> {
    constructor(props) {
        super();
    }

    public render() {
        return <div className="erdiv">
                    <div className="imgError">
                        <img src="http://downloadicons.net/sites/default/files/graduation-icon-66502.png" alt="EPA" />
                    </div>
                    <div className="textError">
                        <h1>
                    {this.props.message} Помилка
                        </h1>
                        <h2>
                            Такої сторінки не існує, або ви ввели неправильний шлях.
                        </h2>
                    </div>
                </div>

    }
}
