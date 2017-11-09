import * as React from 'react';
import { RouteComponentProps } from 'react-router';
//import React, { Component } from 'react';
//import { VirtualizedSelect } from 'react-virtualized-select'
import  VirtualizedSelect  from 'react-virtualized-select'
/*import { View } from 'react-native'
import SelectMultiple from 'react-native-select-multiple'
*/

import 'react-select/dist/react-select.css'
import 'react-virtualized/styles.css'
import 'react-virtualized-select/styles.css'

import 'isomorphic-fetch';

import { Tabbordion, TabPanel, TabLabel, TabContent } from 'react-tabbordion'


interface Specialitys {
    subjects: Subject[];
    univers: Univer[];
    districts: Distryct[];
    selectValueSub: { label: string, value: number }[];
    selectDistrict: { label: string, value: number };
}



interface Distryct {
    name: string;
    id: number;
}

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

export default class ListSpecialties extends React.Component<RouteComponentProps<{}>, Specialitys> {

    constructor()
    {
        super();
        this.state = { subjects: [], univers: [], districts: [], selectValueSub: [], selectDistrict: { value: 0, label: "Всі" } }
    }


    public render() {

        return <div className="col-md-offset-1  col-md-10 col-sm-offset-1 col-sm-10  C col-xs-offset-1">
          
                <Tabbordion animateContent="height" className="accordion" mode="toggle" role="tablist">
                    {this.state.univers.map(univer =>
                        <TabPanel >
                            <TabLabel className="glyphicon "><div className="glyphicon glyphicon-menu-down blockquote h4">Спеціальність: {univer.name} Університет:{univer.university} Область:{univer.district}</div></TabLabel>
                            <TabContent>
                                <div>
                                    <div className="col-md-6"><p>Адреса:{univer.address}</p> <p>Сайт:{univer.site}</p></div>
                                    <div className="col-md-6"><ul>Предмети:{univer.subjects.map(sub => <li> {sub.name}і </li>)} </ul></div>
                                </div>
                            </TabContent>
                        </TabPanel>
                    )}
                </Tabbordion>
                
        </div>
    }
}
