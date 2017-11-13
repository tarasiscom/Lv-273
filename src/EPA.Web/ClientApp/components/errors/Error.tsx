import * as React from 'react';
import { Link, NavLink, RouteComponentProps } from 'react-router-dom'; 
import PropTypes from 'prop-types';

interface Err {
    message: string;
    onRouteChange: PropTypes.func;
}

export class Error404 extends React.Component<RouteComponentProps<{}>&Err, {}> {
    constructor(props) {
        super();

    }

    componentDidMount() {

        this.props.history.listen((location, action) => {
            this.props.onRouteChange();
        });
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
