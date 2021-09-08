import * as React from "react";
import { Link } from "@reach/router";

const HomepageModal = () => {
  return (
    <div class="modal" id="myModal">
      <div class="modal-dialog">
        <div class="modal-content">
          {/* <!-- Modal Header --> */}
          <div class="modal-header">
            <h4 class="modal-title">Warning</h4>
            <button type="button" class="close" data-dismiss="modal">
              &times;
            </button>
          </div>

          {/* <!-- Modal body --> */}
          <div class="modal-body">
            <div style={{ wordWrap: "break-word" }}>
              I haven't included authentication and don't monitor any notes. I apologize for any unfriendly content that could be saved in any
              notes.
            </div>
          </div>

          {/* <!-- Modal footer --> */}
          <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-dismiss="modal">
              Close
            </button>

            <a href="/app/" class="btn btn-primary">
              Continue to Application
            </a>
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomepageModal;
