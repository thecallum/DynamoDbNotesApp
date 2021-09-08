const path = require("path");
module.exports = {
  webpack: {
    alias: {
      "@Components": path.resolve(__dirname, "./src/components/"),
      "@Gateway": path.resolve(__dirname, "./src/gateway/"),
      "@Forms": path.resolve(__dirname, "./src/forms/"),
    },
  },
};
