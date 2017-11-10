import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import Radar from 'react-d3-radar';

interface GeneralDirection{
    id: number;
    name: string;
}

interface GeneralDirectionResult {
    generaldirection: GeneralDirection;
    score: number;
}

interface GeneralDirectionInfo {
    generaldirectionresult: GeneralDirectionResult[];
    loading: boolean;
}

export class ProfTestResult extends React.Component<RouteComponentProps<{}>, GeneralDirectionInfo> {
    constructor() {
        super();
        this.state = {
            generaldirectionresult: [], loading: true
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
                this.setState({generaldirectionresult : data, loading : false});
            });
    }
   drawRadar() {
       return <div className="text-left">
           <Radar className="radar"
               width={600}
               height={600}
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
                   variables: this.state.generaldirectionresult.map(gen =>
                       ({ key: gen.generaldirection.name.toLowerCase(), label: gen.generaldirection.name }),
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
                       values: this.state.generaldirectionresult.reduce((o, generaldirectionresult) =>
                           ({ ...o, [generaldirectionresult.generaldirection.name.toLowerCase()]: generaldirectionresult.score }), {}),
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
        var arrScores = this.state.generaldirectionresult.map(gen => gen.score);
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
