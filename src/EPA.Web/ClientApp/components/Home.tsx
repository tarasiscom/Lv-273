import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import logo from "ClientApp/images/epa1.png";


export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {

        return <div className="pad-for-footer">         
            <section className="main-container">
                <div className="title grey-background col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                    <div className="logo1 col-md-5 col-sm-5 col-xs-5"></div>
                    <p className="caption col-md-5 col-sm-5 col-xs-3">Educational Program Adviser</p>
                </div>
                <div className="main-block">
                    <h2 className="text-center second-title">Обери своє майбутнє разом з EPA</h2>
                    <div className="text-description  col-md-offset-1 col-sm-offset-1 col-xs-offset-1">
                        <p> Ще не знаєш ким хочеш бути в майбутньому? EPA з радістю тобі допоможе. Все, що тобі потрібно: </p>
                        <p>1) Пройти тест на профорієнтацію.</p>
                        <p>2) Вибрати університет до вподоби.</p>
                        <p>3) Діяти! </p>
                    </div>
                </div>
                <h2 className="text-center second-title">Топ-5 університетів</h2>
                <section className="univer-in-row pad-for-footer">
                    <div className="uni-padding col-md-1 "></div>

                    <div className="uni-padding col-md-2 col-sm-6 col-xs-12">
                        <img className="img-univer" src="http://kpi.ua/files/kpi_0.png" width="100%" height="100%" />
                        <p className="text-center text-univer">Київський політехнічний інститут імені ігоря Сікорського</p>
                    </div>
                    <div className="uni-padding col-md-2 col-sm-6 col-xs-12">
                        <img className="img-univer" src="http://vstup.univ.kiev.ua/assets/img/knu.jpg" width="100%" height="100%" />
                        <p className="text-center text-univer">Київський національний університет імені Тараса Шевченка</p>
                    </div>
                    <div className="uni-padding col-md-2 col-sm-6 col-xs-12">
                        <img className="img-univer img-round" src="http://210years.karazin.ua/images/logo-u.png" width="100%" height="100%" />
                        <p className="text-center text-univer">	Харківський національний університет імені В.Н. Каразіна</p>
                    </div>
                    <div className="uni-padding col-md-2 col-sm-6 col-xs-12">
                        <img className="img-univer" src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Kpi.jpg/200px-Kpi.jpg" width="100%" height="100%" />
                        <p className="text-center text-univer">Національний технічний університет "Харківський політехнічний інститут"</p>
                    </div>
                    <div className="uni-padding col-md-2 col-sm-6 col-xs-12 pad-for-footer">
                        <img className="img-univer" src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/de/Nulp_logo_ukr.jpg/280px-Nulp_logo_ukr.jpg" width="100%" height="100%" />
                        <p className="text-center text-univer">Національний університет "Львівська політехніка"</p>
                    </div>
                </section>
            </section>
        </div>
    }
}
