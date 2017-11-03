import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class ChooseUniversity extends React.Component<RouteComponentProps<{}>, {}>
{
    
    renderSelect() {
        return <div>
            <div className="card col-md-3 col-md-offset-2 col-sm-3 col-sm-offset-2 col-xs-8 col-xs-offset-2 pad-for-nav">
                <img className="card-img-top" src="/pictures/profPicture2.jpg" alt="Card image cap" width="100%" ></img>
                <div className="card-block">
                    <h4 className="card-title">Пошук спеціальності по галузі</h4>
                    <p className="card-text">В цьому розділі можли підібрати університет по галузі і спеціальності яка подобається</p>
                    <a href="#" className="btn btn-primary">Рзпочати</a>
                </div>

            </div>
            <div className=" card col-md-3 col-md-offset-2 col-sm-3 col-sm-offset-2 col-xs-8 col-xs-offset-2 pad-for-nav">
                <img className="card-img-top" src="/pictures/profPicture1.jpg" alt="Card image cap" width="100%" ></img>
                <div className="card-block">
                    <h4 className="card-title">Пошук Спеціальності по предметах</h4>
                    <p className="card-text">Цей розділі допоможе підібрати спеціальність по предметах які хочеш здавати на ЗНО</p>
                    <a href="#" className="btn btn-primary">Рзпочати</a>

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