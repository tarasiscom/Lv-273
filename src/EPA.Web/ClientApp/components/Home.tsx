import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ErrorHandlerProp, ResponseChecker } from './App';

//import logo from "ClientApp/images/epa1.png";

interface University
{
    name: string;
    site: string;
    logoId: number;
}

interface Universities
{
    listUniversities: University[],
    imgSrc: string[]
}

export class Home extends React.Component<RouteComponentProps<{}>, Universities> {
    constructor() {

        super();
        this.state = {
            listUniversities: [],
            imgSrc: []
        }
    }

    componentWillMount() {
        this.fetchData();
    }

    private fetchData() {
        fetch('api/Universities/getTopUniversities')
            .then(response => response.json() as Promise<University[]>)
            .then(data => {
                this.setState({
                    listUniversities: data
                });
                //this.fetchImgSrc();
            })
    }

    private fetchImgSrc() {
        fetch('api/Universities/getImgSrc', {
            method: 'POST',
            body: JSON.stringify(this.state.listUniversities),
            headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' }
        })
            .then(response => response.json() as Promise<string[]>)
            .then(data => {
                this.setState({
                    imgSrc: data
                })
            })
    }

    public render() {
        return <div className="pad-for-footer col-md-12">      
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
                <section className="univer-in-row">
                    <div className="uni-padding col-md-1 "></div>
                    {this.state.listUniversities.map((university, id) =>
                        <div className="uni-padding col-md-2 col-sm-6 col-xs-12">
                            <img className="img-univer" src={"api/Universities/" + university.logoId + "/logo"} width="100%" height="100%" />
                            <a className="text-center text-univer" href={university.site}> { university.name }</a>
                        </div>
                        )}
                </section>
            </section>
        </div>
    }
}
