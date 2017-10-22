import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import logo from "ClientApp/images/epa1.png";

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {

        return <div>
            <section className="main-container">
                <div className="title">
                    <div className="logo1"></div>
                    <p className="caption">Educational Program Advisor</p>
                </div>
                <div className = "main-block">
                    <h2 className="text-center second-title">Обери своє майбутнє разом з EPA</h2>
                    <div className="text-description">
                        <p> Ще не знаєш ким хочеш бути в майбутньому? EPA з радістю тобі допоможе. Все, що тобі потрібно: </p>
                        <p>1) Пройти тест на профорієнтацію.</p>
                        <p>2) Вибрати університет до вподоби.</p>
                        <p>3) Діяти! </p>
                    </div>
                </div>
                <h2 className= "text-center second-title">Топ-5 університетів</h2>
                <section className="univer-in-row">
                    <div className= "uni-padding">
                        <img className="img-univer" src="http://kpi.ua/files/kpi_0.png" />
                        <p className = "text-center text-univer">Київський політехнічний інститут імені ігоря Сікорського</p>
                    </div> 
                    <div className="uni-padding">
                        <img className="img-univer" src="http://vstup.univ.kiev.ua/assets/img/knu.jpg" />
                        <p className="text-center text-univer">Київський національний університет імені Тараса Шевченка</p>
                    </div> 
                    <div className="uni-padding">
                        <img className="img-univer img-round" src="http://210years.karazin.ua/images/logo-u.png" />
                        <p className="text-center text-univer">	Харківський національний університет імені В.Н. Каразіна</p>
                    </div> 
                    <div className="uni-padding">
                        <img className="img-univer" src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/b7/Kpi.jpg/200px-Kpi.jpg" />
                        <p className="text-center text-univer">Національний технічний університет "Харківський політехнічний інститут"</p>
                    </div> 
                    <div className="uni-padding">
                        <img className="img-univer" src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/de/Nulp_logo_ukr.jpg/280px-Nulp_logo_ukr.jpg" />
                        <p className="text-center text-univer">Національний університет "Львівська політехніка"</p>
                    </div> 
                </section>
            </section>
        </div>
    }
}
