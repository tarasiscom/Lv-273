import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { ReactPaginate } from 'react-paginate';

import { Tabbordion, TabPanel, TabLabel, TabContent } from 'react-tabbordion'
import { ChooseSpecialtiesBySubject } from './ChooseSpecialtiesBySubject';

interface Subject {
    id: number;
    name: string
}


interface Univer {
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
}

interface SpecialitiInfo
{
    univers: Univer[];
}

export default class ListSpecialties extends React.Component<SpecialitiInfo, {}> {

    constructor(props: SpecialitiInfo)
    {
        super(props);
        this.props = {
            univers: []
        }
    }

    public render() {

        return <div>
            <Tabbordion animateContent="height" className="accordion" mode="toggle" role="tablist">
                {this.props.univers.map(univer =>
                    <TabPanel >
                        <TabLabel className="glyphicon">
                            <div className="glyphicon blockquote h4">
                                <p className="glyphicon glyphicon-menu-down">Спеціальність: {univer.name}</p>
                                <p>Університет: {univer.university}</p>
                                <p>Область: {univer.district}</p>
                            </div>
                        </TabLabel>
                        <TabContent>
                            <div>
                                <div className="col-md-6"><p>Адреса: {univer.address}</p> <p>Сайт: <a href= {univer.site} target = "_blank">{univer.site}</a></p></div>
                                <div className="col-md-6"><ul>Предмети: {univer.subjects.map(sub => <li> {sub.name} </li>)} </ul></div>
                            </div>
                        </TabContent>
                    </TabPanel>
                )}
            </Tabbordion>
        </div>
    }
}
