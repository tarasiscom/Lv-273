import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class ChooseUniversity extends React.Component<RouteComponentProps<{}>, {}>
{
    
    renderSelect() {
        return <div>
            <div className="card col-md-offset-2  col-md-3 col-sm-offset-1 col-sm-4  col-xs-8 col-xs-offset-2 pad-for-nav">
                <img className="card-img-top" src="/pictures/profPicture2.jpg" alt="Card image cap" width="100%" ></img>
                <div className="card-content">
                    <h4 className="card-title">Пошук спеціальності за галуззю</h4>
                    <p className="card-text">Цей розділ допоможе підібрати спеціальність у вищих навчальних закладах за галузями знань.
                        Ви отримаєте інформацію про вищі навчальні заклади в яких проводиться набір на спеціальності за вибраною
                        галуззю знань. Розділ допоможе вибрати ВНЗ за регіоном та рейтингом.</p>
                    <div className="card-read-more">
                        <a href="#" className="btn btn-link btn-block">  Розпочати      </a>
                    </div>
                </div>

            </div>
            <div className="card col-md-offset-2  col-md-3 col-sm-offset-1 col-sm-4 col-xs-8 col-xs-offset-2 pad-for-nav">
                <img className="card-img-top" src="/pictures/profPicture1.jpg" alt="Card image cap" width="100%" ></img>
                <div className="card-content">
                    <h4 className="card-title">Пошук cпеціальності за предметами</h4>
                    <p className="card-text">Цей розділ допоможе підібрати спеціальність у вищих навчальних закладах України за предметами ЗНО.
                        Ви отримаєте інформацію про вищі навчальні заклади в яких проводиться набір на спеціальності за відповідними предметами ЗНО.
                        Розділ допоможе вибрати ВНЗ за регіоном та рейтингом.</p>
                    <div className="card-read-more">
                        <a href="/ChoseSpecBySub" className="btn btn-link btn-block">  Розпочати      </a>
                    </div>
                </div>
            </div>
        </div>
    }
    public render() {
        let content = this.renderSelect();
        return <div>
            {content}
            <div className="col-md-6 col-sm-6 col-xs-12 pad-for-footer2"></div>
        </div>
    }

}