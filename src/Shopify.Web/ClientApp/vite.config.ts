import react from "@vitejs/plugin-react-swc";
import { defineConfig } from "vite";
import mkcert from "vite-plugin-mkcert";
import svgr from "vite-plugin-svgr";
import viteTsconfigPaths from "vite-tsconfig-paths";

export default defineConfig({
  plugins: [
    react(),
    mkcert(),
    svgr({
      svgrOptions: { exportType: "named", ref: true, svgo: false, titleProp: true },
      include: "**/*.svg",
    }),
    viteTsconfigPaths(),
  ],
  build: {
    outDir: "build",
    emptyOutDir: true,
  },
  server: {
    port: 3000,
    https: true,
    proxy: {
      "/api": {
        target: "https://localhost:7089",
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
