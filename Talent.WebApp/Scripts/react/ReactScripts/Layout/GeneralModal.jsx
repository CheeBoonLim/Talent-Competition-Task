import React from 'react'
import ReactDOM from 'react-dom'
import  LoginForm from '../Account/Login.jsx';
import  RegForm  from '../Account/Register.jsx';

export class GeneralModal extends React.Component {
    constructor(props) {
        super(props);
        this.showLoginForm = this.showLoginForm.bind(this);
        this.loadRegiterForm = this.loadRegiterForm.bind(this);
        this.showSignupForm = this.showSignupForm.bind(this);
        this.state = {
            isLogin: true
        }
    };
    showLoginForm() {
        this.setState({
            isLogin: true
        });
    };
    loadRegiterForm() {
        this.setState({
            isLogin: false
        });
    };
    showSignupForm() {
        this.setState({
            isLogin: false
        });
    };
    render() {
        return(
            <div className= "ui tiny modal" id= "generalModal" >
                <div className="content one column stackable center aligned page grid">
                    {this.state.isLogin ? <LoginForm /> : <RegForm />}
                    {this.state.isLogin ? <div className="field text center form-footer"><span className="center aligned">Haven't got account? <a onClick={this.showSignupForm}>Sign up</a></span></div> : <div className="field text center form-footer"><span>Already had an account? <a onClick={this.showLoginForm}>Login</a></span></div>}
                </div>
            </div>
        )
    }
}

//var GeneralModal = React.createClass({
//    componentDidMount: function () {

//    },
//    showLoginForm: function () {
//        this.setState({
//            isLogin: true
//        });
//    },
//    loadRegiterForm: function () {
//        this.setState({
//            isLogin: false
//        });
//    },
//    getInitialState: function () {
//        return {
//            isLogin: true
//        }
//    },
//    showSignupForm: function () {
//        this.setState({
//            isLogin: false
//        });
//    },
//    render: function () {
//        return (
//            <div className="ui tiny modal" id="generalModal">
//                <div className="content one column stackable center aligned page grid">
//                    {this.state.isLogin ? <LoginForm /> : <RegForm />}
//                    {this.state.isLogin ? <div className="field text center form-footer"><span className="center aligned">Haven't got account? <a onClick={this.showSignupForm}>Sign up</a></span></div> : <div className="field text center form-footer"><span>Already had an account? <a onClick={this.showLoginForm}>Login</a></span></div>}
//                </div>
//            </div>
//        )
//    }
//});
