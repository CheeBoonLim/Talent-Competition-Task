import React from 'react';
import ReactDOM from 'react-dom';
import { Banner } from './Banner.jsx';
import { GeneralModal } from './GeneralModal.jsx';
import { CategorySuggestion } from './CategorySuggestion.jsx';
import { ExploreCategories } from './ExploreCategories.jsx';
import { Vision } from './Vision.jsx';
import { Guidelines } from './Guidelines.jsx';

export default class App extends React.Component {
    constructor(props) {
        super(props);
        this.signIn = this.signIn.bind(this);
        this.join = this.join.bind(this);
        this.state = {
            showLoginForm: true
        }
    };
    signIn() {
        this.setState({
            showLoginForm: true
        })
        var signInForm = this.refs.generalModal;
        signInForm.showLoginForm();
        $("#generalModal").modal("show");
    };
    join() {
        this.setState({
            showLoginForm: false
        })
        var signInForm = this.refs.generalModal;
        signInForm.loadRegiterForm();
        $("#generalModal").modal("show");
    };
    componentDidMount() {
        var loginForm = ReactDOM.findDOMNode(this.refs.generalModal);
        $(loginForm).modal({
            detachable: false
        })
    };
    render() {
        return (
            <div>
                <Banner signInFunction={this.signIn} signUpFunction={this.join}/>
                <div id="modal-section">
                    <GeneralModal ref="generalModal" />
                </div>
                <ExploreCategories />
                <CategorySuggestion />
                <Vision />
                <Guidelines />
            </div>
        )
    }
}
