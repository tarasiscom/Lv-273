import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';


export class Footer extends React.Component<{}, {}> {
    public render() {
        return <div className="footer-distributed">
                <div className="footer-right">
                    <a target="_blank" href="https://www.facebook.com/natasha.svystun"><i className="fa fa-facebook"></i></a>
                    <a target="_blank" href="https://twitter.com/svystunlife?lang=uk"><i className="fa fa-twitter"></i></a>
                    <a target="_blank" href="https://www.linkedin.com/in/nataliia-svystun-6a5321a1/"><i className="fa fa-linkedin"></i></a>
                    <a target="_blank" href="https://github.com/tarasiscom/Lv-273"><i className="fa fa-github"></i></a>
                </div>
                <div className="footer-left">
                    <p className="footer-links">
                        <a href="#">Усі Університети</a>
                        ·
                        <Link to={'/profTest'}>Тести</Link>
                        ·
					    <Link to={'/ChooseSpecialty'}> Обрати Спеціальність </Link>
                    </p>
                    <p>Educational Program Adviser &copy; 2017</p>
                </div>
        </div>
    }
}