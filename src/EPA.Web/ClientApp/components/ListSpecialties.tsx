import * as React from 'react';
import { RouteComponentProps } from 'react-router';

import { Tabbordion, TabPanel, TabLabel, TabContent } from 'react-tabbordion'

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

interface Specialties
{
    univers: Univer[];
}

export default class ListSpecialties extends React.Component<Specialties, {}> {

    constructor(props: Specialties)
    {
        super(props);
        this.props = {
            univers: []
        }
    }

    public render() {

        return <div className="panel-group" id="accordion">
            <Tabbordion animateContent="height" className="accordion" mode="toggle" role="tablist">
            {this.props.univers.map(univer =>
                    <TabPanel  className="panel panel-default">
                        <TabLabel  className="panel-heading">
                        <h4 className="panel-title"> {univer.name} </h4>
                            <p>Університет: {univer.university}</p>
                            <p>Область: {univer.district}</p>
                        </TabLabel >
                
                        <TabContent id="collapse1" className="panel-collapse collapse">
                        <div className="panel-body">
                            <div className="col-md-6"><p>Адреса: {univer.address}</p> <p>Сайт: {univer.site}</p></div>
                            <div className="col-md-6"><ul>Предмети: {univer.subjects.map(sub => <li> {sub.name} </li>)} </ul></div>
                        </div>
                        </TabContent>
                    </TabPanel>
            )}
            </Tabbordion>  
        </div>
    }
}
