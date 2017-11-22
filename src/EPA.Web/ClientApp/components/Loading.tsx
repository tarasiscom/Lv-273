import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';

export class Loading extends React.Component<{}, {}> {
    public render() {
        return (<div className="pad-for-footer">
            <div className="loadScreen">
                <div className="loadImgOpac">
                    <div className="loadImgMove">
                        <div className="loadImgRotate"> <span> </span>  </div>
                    </div>
                </div>
            </div></div>)
    }
}