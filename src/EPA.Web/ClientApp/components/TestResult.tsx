/*import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

export class TestResult extends React.Component<RouteComponentProps<{}>, ResultsInfo> {
    constructor() {
        super();
        this.state = {
             profDirection: "", profSpecialties: [], loading: true
        };

    }
    componentDidMount() {
        this.fetchData()
    }
    fetchData() {

        fetch("api/profTest/3/result", {
            method: 'POST',
            body: JSON.stringify(7),
            headers: {
                'Content-Type': 'application/json'
            }
        }).then(response => response.json() as Promise<ResultsInfo>)
            .then(data => {
                this.setState({ profDirection: data.profDirection, profSpecialties: data.profSpecialties, loading: false });
            });
    }
    public render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderResult();
        return <div>{contents}</div>
    }
    private renderResult() {

        return <section className="main-settings">
            <div className="rec-center">
                <h1 className="text-center uni-recom"> Ваш результат - {this.state.profDirection}</h1>
                <h2 className="uni-recom recom-padding rec-pad-left">Наші рекомендації:</h2>
                <section className='rec-pad-left'>
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>Спеціальність</th>
                                <th>Університет</th>
                                <th>Область</th>
                                <th>Адреса</th>
                                <th>Сайт</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.profSpecialties.map(cont =>
                                <tr>
                                    <td>{cont.specialtyName}</td>
                                    <td>{cont.university}</td>
                                    <td>{cont.district}</td>
                                    <td>{cont.address}</td>
                                    <td>{cont.site}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
            </section>
            </div>
        </section>
    }
}



interface ResultsInfo {
    profSpecialties: UserSpeciality[];
    profDirection: string;
    loading: boolean;
}
interface UserSpeciality {
    specialtyName: string;
    university: string;
    district: string;
    address: string;
    site: string;
}
*/