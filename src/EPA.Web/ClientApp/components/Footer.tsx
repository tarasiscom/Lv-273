import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';


export class Footer extends React.Component<{}, {}> {
    public render() {
        
        return <footer className="footer">
            <footer className="footer-distributed">

                <div className="footer-right">

                    <a href="#"><i className="fa fa-facebook"></i></a>
                    <a href="#"><i className="fa fa-twitter"></i></a>
                    <a href="#"><i className="fa fa-linkedin"></i></a>
                    <a href="#"><i className="fa fa-github"></i></a>

                </div>

                <div className="footer-left">

                    <p className="footer-links">
                        <a href="#">Усі Університети</a>
                        ·
					<a href="#">Тести</a>
                        ·
					<a href="#">Обрати Університет</a>
                        
                    </p>

                    <p>Educational Profession Adviser &copy; 2017</p>
                </div>

            </footer>
        </footer>

    }
}