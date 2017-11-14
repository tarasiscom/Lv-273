import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import { Tabbordion, TabPanel, TabLabel, TabContent } from 'react-tabbordion';

interface Subject {
    id: number;
    name: string
}

interface Specialty {
    name: string;
    university: string;
    address: string;
    district: string;
    site: string;
    subjects: Subject[];
}

interface Specialties
{
    specialties: Specialty[];
}

export default class ListSpecialties extends React.Component<Specialties, {} > {

    constructor(props) {
        super(props);
    }

    public render() {

        return <div className="col-md-offset-1  col-md-10 col-sm-offset-1 col-sm-10  C col-xs-offset-1">
            <Tabbordion animateContent="height" className="accordion" mode="toggle" role="tablist">
                {this.props.specialties.map((specialty, id) =>
                    <TabPanel key={id} className="my-cursor">
                        <div className="panel panel-default">
                            <div className="panel-heading">
                                <TabLabel className="my-cursor">
                                    <p>Спеціальність: {specialty.name} </p>
                                    <p>Університет: {specialty.university}</p>
                                    <p>Область: {specialty.district}</p>
                                </TabLabel >
                            </div>
                         <TabContent>
                                <div className="panel-body">
                                    <div className="col-md-6">
                                        <p>Адреса: {specialty.address}</p>
                                        <p>Сайт:
                                         <a target="_blank" href={specialty.site}> {specialty.site} </a>
                                        </p>
                                    </div>
                                    <div className="col-md-6">
                                        <ul>Предмети: {specialty.subjects.map((sub, id) =>
                                            <li key={id}> {sub.name} </li>)}
                                        </ul>
                                    </div>
                                </div>
                        </TabContent>
                      </div> 
                    </TabPanel>
                )}
            </Tabbordion>
        </div>
    }
}
