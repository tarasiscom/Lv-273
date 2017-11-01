import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class ChooseUniversity extends React.Component<RouteComponentProps<{}>, {}>
{
    beginRenerBySpecial() { /*Call render*/};
    beginRenerBySubj() { /*Call render*/} 
    public render() {
        return <div>
            <div className="card col-md-5 col-sm-5 col-xs-12">
                <img className="card-img-top" src="..." alt="Card image cap"></img>
                <div className="card-block">
                    <h4 className="card-title">Пошук спеціальності по галузі</h4>
                    <p className="card-text">В цьому розділі можли підібрати університет по галузі і спеціальності яка подобається</p>
                    <button className="btn btn-primary" onClick={() => this.beginRenerBySpecial()}>
                        Розпочати
                    </button>
                </div>

            </div>
            <div className="card col-md-5 col-sm-5 col-xs-12">
                <img className="card-img-top" src="..." alt="Card image cap"></img>
                <div className="card-block">
                    <h4 className="card-title">Пошук Спеціальності по предметах</h4>
                    <p className="card-text">В Цьлму розділі допоможе підібрати по предметах які хочеш здавати на ЗНО</p>
                    <button className="btn btn-primary" onClick={() => this.beginRenerBySubj()}>
                        Розпочати
                    </button>
                    
                </div>

            </div>
        </div>
    }

}