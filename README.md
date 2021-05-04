# MaterialsVR

MaterialsVR lets you interact with molecular models in Virtual Reality. Add planes to create cross section. See electron density isosurfaces and load your own CHGCAR files.
## Features
### Toolbar
Start by selecting a molecule from the menu. Each model is accompanied by a toolbar above. Use it to enable rotation, disable bounding box, or add a plane that intersects with the molecule.

| Button     | Description                                                                                                                                |
|------------|--------------------------------------------------------------------------------------------------------------------------------------------|
| Rotation   | Let the entire model start rotating at a constant speed                                                                                    |
| Add Plane  | Add a semitransparent intersecting plane to the model. It can be deleted or set as the cross section plane (only render model on one side) |
| Bound box  | Enable the bounding box to move/rotate/adjust the size of the model. Disable the bounding box to look up atom names.                       |
| Polyhedral | Toggle polyhedral view (on select models)                                                                                                  |
| Delete     | Remove the model                                                                                                                           |
| Sliders    | Adjust the atom size or isosurface level (only on isosurface models)                                                                       |

Click on "Tool Tips" to show more insights.
### Cross Section View
You can set a plane to be the cross section plane by clicking the button on top. Only meshes on one side of the plane will be rendered.
### Electron density isosurfaces
Click on the "Molecule" title bar to switch to isosurface list. From there, you may select a isosurface model generated from CHGCAR files. You can modify the isosurface level in the toolbar.

## About
- Unity version: 2019.4.14f1
- Steam page: https://store.steampowered.com/app/1564310/Materials_VR/
- Prof. Dr. André Schleife: http://schleife.matse.illinois.edu/
- Author: Zhili Luo [@jeffreyluo98](https://github.com/jeffreyluo98)
## Acknowledgement
We acknowledge contributions by (in alphabetical order) Siddharth Ahuja, Emi Caroline Brown, Andong Jing, Sean Lin, Qiaoqian Lin, Noah Rebei, Sujay Shah, Zekun Wei, Jinlin Xu, Zhongshen Zeng, and Xusheng Zhang and support from the NCSA SPIN program, the NSF REU INCLUSION (grant No. OAC-1659702), and the NSF CAREER grant No. DMR-1555153 for support.
