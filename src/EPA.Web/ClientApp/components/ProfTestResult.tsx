import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Radar from 'react-d3-radar';

interface GeneralDirectionResult {
    name: string;
    score: number;
}
interface GeneralDirectionInfo {
    generaldirection: GeneralDirectionResult[];
    loading: boolean;
}
export class ProfTestResult extends React.Component<RouteComponentProps<{}>, GeneralDirectionInfo> {
    constructor() {
        super();
        this.state = {
            generaldirection: [], loading: true
        };
    }
    
   componentDidMount() {
        this.fetchData()
    }
   fetchData() {
        let path = "api/profTest/"+this.props.match.params['id']+"/resultnew";
        fetch(path)
            .then(response => response.json() as Promise<GeneralDirectionResult[]>)
            .then(data => {
                this.setState({generaldirection : data, loading : false});
            });
    }
   drawRadar() {
       return <div className="text-center">
           <Radar className="radar"
               width={500}
               height={500}
               padding={70}
               domainMax={this.GetDomainMax()}
               highlighted={null}
               onHover={(point) => {
                   if (point) {
                       console.log('hovered over a data point');
                   } else {
                       console.log('not over anything');
                   }
               }}

               data={{
                   variables: this.state.generaldirection.map(gen =>
                       ({ key: gen.name.toLowerCase(), label: gen.name }),
                   ),
                   sets:
                   [{
                       key: 'user',
                       label: 'User Results',
                       /*values: this.state.generaldirection.reduce(function (acc, generaldirection) {
                           acc[generaldirection.name.toLowerCase()] = generaldirection.score;
                           return acc;
                       }, {}),
                       */
                       values: this.state.generaldirection.reduce((o, generaldirection) => ({ ...o, [generaldirection.name.toLowerCase()]: generaldirection.score }), {}),
                   },
                   ],

               }}

           />
       </div>
   }
    public render() {
        let content = this.state.loading
            ? <p><em>Loading...b</em></p>
            : this.drawRadar();
            
        return <div>{content}</div>;
   }
    private GetDomainMax() {
        var arrScores = this.state.generaldirection.map(gen => gen.score);
        var max = 0;
        for (let i = 0; i < arrScores.length; i++) {
            if (arrScores[i] > max)
            {
                max = arrScores[i];
            }
        }
        max = (max & 1) == 0 ? max : max+1;
        return max;
    }
}
