import axios from "axios";

export default {
  props: ["itemId"],
  data() {
    return {
      file: null,
      success: null,
      error: null,
    };
  },
  computed: {
    id: function() {
      return this.$props.itemId;
    },
    isIdInvalid: function() {
      return !/^\+?(0|[1-9]\d*)$/.test(this.id);
    },
  },
  methods: {
    upload() {
      this.reset();
      debugger;
      let formData = new FormData();
      formData.append('file', this.file);
      axios
        .post(
          `/upload?itemId=${this.id}`,formData
        )
        .then(() => {
          this.success = true;
        })
        .catch((err) => {
          this.success = false; 
          if (err.response && err.response.data) {
            this.error = err.response.data.Message ?? err.response.data;
          }
        });
    },
    fileUploaded() {
      this.reset();
    },
    reset() {
      this.success = null;
      this.error = null;
    },
  },
};
