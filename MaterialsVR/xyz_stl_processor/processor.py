import bpy
import sys
import os

from sys import argv


# delete initialized default objects
def delete_all_objects():
    bpy.ops.object.select_all(action='DESELECT')
    bpy.data.objects['Cube'].select = True
    bpy.ops.object.delete()


# process STL file
def process_stl():
    delete_all_objects()
    bpy.ops.import_mesh.stl(filepath=file_name)
    bpy.ops.export_scene.fbx(filepath=molecule_name + '.fbx')
    print("Export fbx file successfully")


# process XYZ file
def process_xyz():
    delete_all_objects()
    bpy.ops.wm.addon_install(filepath=os.getcwd() + '/io_mesh_xyz.zip')
    bpy.ops.wm.addon_enable(module='io_mesh_xyz')

    bpy.ops.import_mesh.xyz(filepath=molecule_name + '.xyz')
    bpy.ops.export_scene.fbx(filepath=molecule_name + '.fbx')
    print("Export fbx file successfully")


def check_file_type():
    if file_name.split('.')[-1] == 'xyz':
        process_xyz()
    elif file_name.split('.')[-1] == 'stl':
        process_stl()
    else:
        raise ValueError('File is missing or file type incorrect!')


if __name__ == "__main__":
    # we get blend file path
    file_path = bpy.data.filepath

    # we get the directory relative to the blend file path
    dir = os.path.dirname(file_path)

    # we append our path to blender modules path
    # we use if not statement to do this one time only
    if dir not in sys.path:
        sys.path.append(dir)

    file_name = argv[-1]
    molecule_name = file_name.split('.')[0]

    # main routine
    check_file_type()
